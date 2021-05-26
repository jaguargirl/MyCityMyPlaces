let map, marker
// Initialize and add the map
function initMap() {

    // The location of Romanian Palace of Parliament
    const parliament = { lat: 44.4232, lng: 26.0858 };
    // The map, centered at Parliament
    map = new google.maps.Map(document.getElementById("map"), {
    zoom: 16,
    center: parliament,
    });
    // The marker, positioned at Parliament
    marker = new google.maps.Marker({
    position: parliament,
    map: map,
    });
    let locs = document.getElementById("locations").innerText;
    if(locs.length>0){
        var locArr = locs.split(";");
        locArr.forEach(getCoord);
        function getCoord(item){
            var coord = item.split(",");
            if(coord.length>1){
                const place = { lat: parseFloat(coord[0]), lng: parseFloat(coord[1]) };
                new google.maps.Marker({
                    position: place,
                    map: map,
                });
                map.panTo(place);
                console.log(place);
            }

        }
    }
    map.addListener("click", (e) => {
        placeMarkerAndPanTo(e.latLng, map);
    });
    var infoWindow = new google.maps.InfoWindow();
    const locationButton = document.createElement("button");
    locationButton.textContent = "Current Location";
    locationButton.classList.add("custom-map-control-button");
    map.controls[google.maps.ControlPosition.TOP_CENTER].push(locationButton);
    locationButton.addEventListener("click", () => {
        // Try HTML5 geolocation.
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    const pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,
                    };
                    infoWindow.setPosition(pos);
                    infoWindow.setContent("Location found.");
                    infoWindow.open(map);
                    map.setCenter(pos);
                },
                () => {
                    handleLocationError(true, infoWindow, map.getCenter());
                }
            );
        } else {
            // Browser doesn't support Geolocation
            handleLocationError(false, infoWindow, map.getCenter());
        }
    });
}
function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(
        browserHasGeolocation
            ? "Error: The Geolocation service failed."
            : "Error: Your browser doesn't support geolocation."
    );
    infoWindow.open(map);
}
function placeMarkerAndPanTo(latLng, map) {
    if (marker && marker.setMap) {
        marker.setMap(null);
    }
    marker = new google.maps.Marker({
        position: latLng,
        map: map,
    });
    map.panTo(latLng);
}
function changeText(){
    var on = document.getElementById("shared").checked;
    if(on!==true){
        document.getElementById("cktxt").innerHTML="No";
    }else{
        document.getElementById("cktxt").innerText="Yes";
    }
}
function addLocation(){
    let longlat = marker.getPosition();
    let obj = longlat.toJSON();
    document.getElementById("lon").value = obj.lng.toString();
    document.getElementById("lat").value = obj.lat.toString();
    document.getElementById("shared").value = document.getElementById("shared").checked;
    /*
    new google.maps.Marker({
        position: longlat,
        map: map,
    });
    map.panTo(longlat);
     */
}

$('#exampleModal').on('show.bs.modal', function (event) {
    let button = $(event.relatedTarget) // Button that triggered the modal
    let recipient = button.data('whatever') // Extract info from data-* attributes
    // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
    // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
    let modal = $(this)
    modal.find('.modal-title').text('My message ' + recipient)
    modal.find('.modal-body input').val(recipient)
})
