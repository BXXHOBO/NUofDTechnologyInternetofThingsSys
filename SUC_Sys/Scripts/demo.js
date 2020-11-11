
function initDemoMap(){

    var Esri_WorldImagery = L.tileLayer('http://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Tiles &copy; Esri &mdash; Source: Esri, i-cubed, USDA, USGS, ' +
        'AEX, GeoEye, Getmapping, Aerogrid, IGN, IGP, UPR-EGP, and the GIS User Community'
    });

    var Esri_DarkGreyCanvas = L.tileLayer(
        "http://{s}.sm.mapstack.stamen.com/" +
        "(toner-lite,$fff[difference],$fff[@23],$fff[hsl-saturation@20])/" +
        "{z}/{x}/{y}.png",
        {
            attribution: 'Tiles &copy; Esri &mdash; Esri, DeLorme, NAVTEQ, TomTom, Intermap, iPC, USGS, FAO, ' +
            'NPS, NRCAN, GeoBase, Kadaster NL, Ordnance Survey, Esri Japan, METI, Esri China (Hong Kong), and the GIS User Community'
        }
    );
    var normalMap = L.tileLayer.chinaProvider('Baidu.Normal.Map', {
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
        layers: [normalMap]

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