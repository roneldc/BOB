var app = angular.module("myApp", ['uiGmapgoogle-maps']);

app.controller("mapController", function ($scope, $http) {
    $scope.map = { center: { latitude: 14.638234, longitude: 121.042771 }, zoom: 16 }
    
    $scope.markers = [];
    $scope.locations = [];

 //$http.get('/MAP/GetAllLocation').then(function (data) {

 //       $scope.locations = data.data;

 //    }, function () {
 //       alert('Error');
 //   });

 
  $http.get('/MAP/GetAllLocationMarker').then(function (data) {

         $scope.locations = data.data;
        var markers = data.data;
        _.forEach(markers, function (marker) {
            marker.id = marker.Row,
            marker.coords = {
                latitude: marker.lat,
                longitude: marker.longi
                
            }
            marker.name = marker.SYSTEMS,
            marker.municipality = marker.MUNICIPALITY,
            marker.sa = marker.SERVICE_ORIGINAL,
            marker.fusa = marker.SERVICE_FIRMED,
            marker.image = marker.ImagePath

        })
        $scope.markers = markers;


    }, function () {
        alert('Error');
    });

 

  //$http.get('/MAP/GetAllLocationMarker').then(function (data) {

  //    $scope.locations = data.data;
  //    var markers = data.data;
  //    _.forEach(markers, function (marker) {
  //        marker.id = markers.Row,
  //        marker.coords = {
  //            latitude: 14.638234,
  //            longitude: 121.042771
  //        }

  //    })
  //    $scope.markers = markers;


  //}, function () {
  //    alert('Error');
  //});

  
   
    $scope.ShowLocation = function (Row) {

        $http.get('/MAP/GetMarkerInfo', {

            params: {
                Row: Row
            }

        }).then(function (data) {
            $scope.markers = [];
         
            $scope.markers.push({

                id: data.data.Row,

                coords: { latitude: data.data.lat, longitude: data.data.longi },
                title: data.data.SYSTEMS,
                municipality: data.data.MUNICIPALITY,
                idsys: data.data.IDSystem,
                sa: data.data.SERVICE_ORIGINAL,
                fusa: data.data.SERVICE_FIRMED,
                oa: data.data.AREA_OPERATIONAL,
                noa: data.data.AREA_NONOPERATIONAL
                
               // address: data.data.Address,
             //   image: data.data.ImagePath

            });
           

            $scope.map.center.latitude = data.data.lat;
            $scope.map.center.longitude = data.data.longi;
           
        }, function () {

            alert('Error');

        });

    }

    //$scope.showInfo = function () {
    //    logInfo('Button clicked!');
    //    //$window.alert("hellow");
    //}


    $scope.windowOptions = {

        show: true

    };



});
