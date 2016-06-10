"use strict";

BattleShipApp.controller("AllGamesStatsController", function ($scope, $http) {
    $scope.loading = true;
    $scope.data = [];
    $http.get(document.location.origin + "/api/Stats/All").success(function (data) {
        $scope.data = data.ResponseData || [];
        $scope.loading = false;
        console.log(data);
    });
});