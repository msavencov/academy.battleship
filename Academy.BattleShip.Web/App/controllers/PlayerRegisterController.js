"use strict";

BattleShipApp.controller("PlayerRegisterController", ["$scope", "$rootScope", function ($scope, $rootScope) {
    $scope.Name = "";
    $scope.Key = "";
    $scope.model = {};
    
    $scope.reset = function() {
        $scope.iterate(function(x, y) {
            $scope.model.ShipMap[x][y] = false;
        });
    };

    $scope.iterate = function(callback) {
        for (var i = 0; i < 10; i++) {
            for (var j = 0; j < 10; j++) {
                callback(j, i);
            }
        }
    };

    $scope.toggle = function(x, y) {
        $scope.model.ShipMap[x][y] = !$scope.model.ShipMap[x][y];
    };
    
    // ReSharper disable once UndeclaredGlobalVariableUsing 
    // must be declared in head script section
    $scope.model = playerModel;
}]);
