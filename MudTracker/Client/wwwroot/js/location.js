let dotNetHelper = null;
const options = {
    enableHighAccuracy: true,
    timeout: 5000,
    maximumAge: 0
};

window.getLocation = (helper) =>
{
    dotNetHelper = helper;
    navigator.geolocation.getCurrentPosition(success, error, options);
};

function success(pos) {
    var crd = pos.coords;

    console.log(crd);

    dotNetHelper.invokeMethodAsync('SetLocationAsync', crd.latitude, crd.longitude);
}

function error(err) {
    console.warn(`ERROR(${err.code}): ${err.message}`);
}
