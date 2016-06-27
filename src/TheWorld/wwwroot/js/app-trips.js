﻿//app trips

(function () {
    "use strict";
    angular.module("app-trips", ["ngRoute"])
    .config(function ($routeProvider) {
        $routeProvider.when("/", {
            controller: "tripsController",
            controllerAs: "vm",
            templateUrl:"/views/tripsView.html"
        });

        $routeProvider.when("/editor/:tripName", {
            controller: "tripEditorController",
            controllerAs: "vm",
            templateUrl:"/views/tripEditorView.html"
        });

        $routeProvider.otherwise({
            redirectTo:"/"

        });
    });
    //creates a module
})();//IFFE takes the object out of the global scope