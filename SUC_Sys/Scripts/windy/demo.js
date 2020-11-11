
function initDemoMap(){

    var Esri_WorldImagery = L.tileLayer('http://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}', {

    });

    var Esri_DarkGreyCanvas = L.tileLayer(
        "http://{s}.sm.mapstack.stamen.com/" +
        "(toner-lite,$fff[difference],$fff[@23],$fff[hsl-saturation@20])/" +
        "{z}/{x}/{y}.png",
        {

        }
    );
    var normalMap = L.tileLayer("http://t2.tianditu.cn/img_w/wmts?service=wmts&request=GetTile&version=1.0.0&LAYER=img&tileMatrixSet=w&TileMatrix={z}&TileRow={y}&TileCol={x}&style=default&format=tiles" {
        maxZoom: 18,
        minZoom: 5
    });
    var satelliteMap = L.tileLayer.chinaProvider('Baidu.Satellite.Map', {
        maxZoom: 18,
        minZoom: 5
    });
    var  annotionMap = L.tileLayer.chinaProvider('Baidu.Satellite.Annotion', {
            maxZoom: 18,
            minZoom: 5
        });

    var baseLayers = {
        "Baidumap": normalMap,
        "BaiduSatellite": satelliteMap,
        "Satellite": Esri_WorldImagery,
        "Grey Canvas": Esri_DarkGreyCanvas
    };

    var overlayLayers = {
        "label": annotionMap
    }
    
  
    var map = L.map('map', {
        //layers: [Esri_WorldImagery]
        layers: [normalMap],
        'attributionControl': false
    });


    var layerControl = L.control.layers(baseLayers, overlayLayers);
    layerControl.addTo(map);
    map.setView([50.00, 14.44], 3);

    return {
        map: map,
        layerControl: layerControl
    };
}

// demo map
var mapStuff = initDemoMap();
var map = mapStuff.map;
var layerControl = mapStuff.layerControl;
var handleError = function(err){
    console.log('handleError...');
    console.log(err);
};

WindJSLeaflet.init({
	localMode: true,
	map: map,
	layerControl: layerControl,
	useNearest: false,
	timeISO: null,
	nearestDaysLimit: 7,
	displayValues: true,
	displayOptions: {
		displayPosition: 'bottomleft',
		displayEmptyString: 'No wind data'
	},
	overlayName: 'wind',

	// https://github.com/danwild/wind-js-server
	//pingUrl: 'http://localhost:16267/alive',
	//latestUrl: 'http://localhost:16267/latest',
	//nearestUrl: 'http://localhost:16267/nearest',
	errorCallback: handleError
});