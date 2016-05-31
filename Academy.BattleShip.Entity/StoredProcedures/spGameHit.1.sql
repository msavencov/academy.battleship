/****** Object:  StoredProcedure [dbo].[spGameHit]    Script Date: 2016-05-31 21:48:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[spGameHit]
	@secretKey char(10),
	@IdPlayer2 int,
	@X_Pos tinyint,
	@Y_Pos int
as begin
	DECLARE	
		@IdPlayer INT,
		@isRegistered BIT = 0,
		@GameId INT,
		@EndDate DATETIME,
		@QtyGameHits TINYINT,
		@QtySuccesfullHits TINYINT,
		@MapShipsId INT,
		@ShipCellsId INT,
		@MapShipType TINYINT,
		@GameShipHits TINYINT,
		@isHit BIT = 0,
		@isError bit,
		@Message varchar(400)

	SELECT 
		@IdPlayer = IdPlayer,
		@isRegistered = isRegistered
	FROM Players (nolock)
	WHERE SecretKey = @secretKey

	IF @IdPlayer IS NULL 
		BEGIN
			SELECT @isError = 1, @Message = 'User is not identified with provided secret key. Please try another key'
			RETURN;
		END

	IF ISNULL(@isRegistered, 0) = 0
		BEGIN
			SELECT @isError = 1, @Message = 'Current user is not registered. Please register your account'
			RETURN;
		END
	
	SELECT @GameId = Id, @EndDate = End_DT, @QtyGameHits = QtyHits, @QtySuccesfullHits = QtySuccessfullHits
	FROM 
		PlayerGames
	WHERE 
		PlayerId1 = @IdPlayer AND
		PlayerId2 = @IdPlayer2

	IF 
		(@GameId IS NOT NULL AND @QtySuccesfullHits >= 20)
		OR 
		(@GameId IS NOT NULL AND @QtyGameHits >= 100)
		BEGIN
			SELECT @isError = 1, @Message = 'Game is already ended. Please try another game'
			RETURN;
		END
	
	IF @GameId IS NULL
	BEGIN
		INSERT PlayerGames (PlayerId1, PlayerId2, Start_DT, End_DT, QtyHits, QtySuccessfullHits)
		VALUES (@IdPlayer, @IdPlayer2, GETDATE(), NULL, 0, 0)

		SELECT @GameId = Id FROM PlayerGames WHERE Id = SCOPE_IDENTITY() AND @@ROWCOUNT > 0 AND PlayerId1 = @IdPlayer AND PlayerId2 = @IdPlayer2
	END


	SELECT 
		@MapShipsId = MAX(ms.Id),
		@ShipCellsId = MAX(sc.Id)
	FROM MapShips ms 
		INNER JOIN ShipCells sc on sc.ShipId = ms.Id
	WHERE 
		ms.PlayerId = @GameId AND
		sc.X_Pos = @X_Pos AND
		sc.Y_Pos = @Y_Pos;

	IF @MapShipsId IS NOT NULL
		SELECT @MapShipType = ShipType FROM MapShips WHERE Id = @MapShipsId

	IF @ShipCellsId IS NULL
		BEGIN
			SELECT @isError = 0, @Message = 'Missed'
		END
	ELSE IF @ShipCellsId IS NOT NULL AND EXISTS (SELECT TOP 1 1 FROM GameHits WHERE ShipCellId = @ShipCellsId AND GameId = @GameId)
		BEGIN
			SELECT 
				 @GameShipHits = COUNT(DISTINCT sc.Id) 
			FROM GameHits gh
				INNER JOIN ShipCells sc on sc.Id = gh.ShipCellId
			WHERE 
				gh.GameId = @GameId AND 
				sc.ShipId = @MapShipsId 
			
			IF @GameShipHits + 1 < @MapShipType	
				SELECT @isError = 0, @Message = 'Hit', @isHit = 1

			IF @GameShipHits + 1 >= @MapShipType
				SELECT @isError = 0, @Message = 'Dead', @isHit = 1		
		END
	
	INSERT GameHits (GameId, ShipCellId, X_Pos, Y_Pos)
	VALUES (@GameId, @ShipCellsId, @X_Pos, @Y_Pos)

	UPDATE 
		PlayerGames
	SET
		QtyHits = QtyHits + 1,
		QtySuccessfullHits = QtySuccessfullHits + @isHit,
		End_DT = CASE WHEN (QtyHits + 1) >= 100 OR (QtySuccessfullHits + @isHit) >= 20 THEN GETDATE() ELSE End_DT END
	WHERE 
		Id = @GameId AND
		PlayerId1 = @IdPlayer AND
		PlayerId2 = @IdPlayer2

		--SELECT @GameId, @IdPlayer, @IdPlayer2
	
	IF (@QtyGameHits + 1) >= 100 OR (@QtySuccesfullHits + @isHit) >= 20
		SELECT @isError = 0, @Message = 'Game end'

	select @isError IsError, @Message Mesasge
END
