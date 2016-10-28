var app = angular.module("WFBApp", []);
app.controller("FileBrowserController", function ($scope, $http) {
    $scope.showDisks = true;

    //Get disk names
    $http.get('api/FileBrowser').
    success(function (data, status, headers, config) {
        $scope.diskNames = data;
    }).
    error(function (dara, status, headers, config) {
        alert("Error");
    });

    //Get files with new path
    $scope.newPath = function (path) {
        $http.get('api/FileBrowser/', {
            params: { path: path }
        }).
        success(function (data, status, headers, config) {
            $scope.folder = data;
            $scope.showDisks = false;

        }).
        error(function (data, status, headers, config) {
            alert("Error");
        });
    }

    $scope.back = function () {

        //geting backpath substring
        var bpath = $scope.folder.Path.substring(0, $scope.folder.Path.length - $scope.folder.Name.length - 1);

        //bpath could be like C: or D: etc.
        if (bpath.length == 2) {
            bpath += "\\";
            //bpath = C:\, D:\ etc.
        };
        $http.get('api/FileBrowser/', {
            params: { path: bpath }
        }).
        success(function (data, status, headers, config) {
            $scope.folder = data;
        }).
        error(function (data, status, headers, config) {
            alert("Error");
        });
    }


    $scope.backShow = function () {
        if ($scope.folder.Path.length <= 3) {
            return false;
        }
        else {
            return true;
        }
    }

    //getting short name of folder or file
    $scope.shortName = function (file) {
        file = file.substring(file.lastIndexOf('\\') + 1);
        return file;
    }

});