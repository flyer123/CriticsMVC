function include(scriptUrl) {
    document.write('<script src="' + scriptUrl + '"></script>');
}

function isIE() {
    var myNav = navigator.userAgent.toLowerCase();
    return (myNav.indexOf('msie') != -1) ? parseInt(myNav.split('msie')[1]) : false;
};

/* cookie.JS
 ========================================================*/
include('js/jquery.cookie.js');

/* Easing library
 ========================================================*/
include('js/jquery.easing.1.3.js');

/* PointerEvents
 ========================================================*/
;
(function ($) {
    if(isIE() && isIE() < 11){
        include('js/pointer-events.js');
        $('html').addClass('lt-ie11');
        $(document).ready(function(){
            PointerEventsPolyfill.initialize({});
        });
    }
})(jQuery);

/* Stick up menus
 ========================================================*/
;
(function ($) {
    var o = $('html');
    if (o.hasClass('desktop')) {
        include('js/tmstickup.js');

        $(document).ready(function () {
            $('#stuck_container').TMStickUp({})
        });
    }
})(jQuery);

/* Stacktable
 ========================================================*/
;
(function ($) {
    var o = $('.stacktable');
    if (o.length > 0) {
        include('js/stacktable.js');

        $(document).ready(function () {
            $('.stacktable').stacktable();
        });
        
    }
})(jQuery);

/* Owl Carousel
========================================================*/
;(function ($) {
    var o = $('.owl-carousel');
    if (o.length > 0) {
        include('js/owl.carousel.min.js');
        $(document).ready(function () {
            $('.owl-carousel1').owlCarousel({
                margin: 30,
                smartSpeed: 450,
                loop: true,
                dots: true,
                dotsEach: 1,
                nav: false,
                navClass: ['owl-prev fa fa-angle-left', 'owl-next fa fa-angle-right'],
                responsive: {
                    0: { items: 1 },
                    768: { items: 2},
                    980: { items: 3}
                }
            });

            $('.owl-carousel2').owlCarousel({
                margin: 30,
                smartSpeed: 450,
                loop: true,
                dots: false,
                dotsEach: 1,
                nav: true,
                navClass: ['owl-prev fa fa-angle-left', 'owl-next fa fa-angle-right'],
                responsive: {
                    0: { items: 1 },
                    768: { items: 1},
                    980: { items: 1}
                }
            });


        });
    }
})(jQuery);

/* Camera
========================================================*/
;(function ($) {
var o = $('#camera');
    if (o.length > 0) {
        if (!(isIE() && (isIE() > 9))) {
            include('js/jquery.mobile.customized.min.js');
        }

        include('js/camera.js');

        $(document).ready(function () {
            o.camera({
                autoAdvance: true,
                height: '12.9756%',
                minHeight: '250px',
                pagination: true,
                thumbnails: false,
                playPause: false,
                hover: false,
                loader: 'none',
                navigation: false,
                navigationHover: false,
                mobileNavHover: false,
                fx: 'simpleFade'
            })
        });
    }
})(jQuery);


/* Dropzone
========================================================*/

// ;(function ($) {

    // include('js/dropzone.js');
    
   // $(document).ready(function () {
      // Dropzone.autoDiscover = false;
      // //Simple Dropzonejs 
      // $("#dZUpload").dropzone({
          // url: "hn_SimpeFileUploader.ashx",

            // maxFilesize: 1, // MB
            // maxFiles: 10,
            // parallelUploads: 5,
            // addRemoveLinks: true,
            // acceptedFiles: "image/*",
            // dictRemoveFile: "Удалить",
            // dictFileTooBig: "File is too big ({{filesize}}MiB). Max filesize: {{maxFilesize}}MiB.",

          // success: function (file, response) {
              // var imgName = response;
              // file.previewElement.classList.add("dz-success");
              // //console.log('Successfully uploaded :' + imgName);
          // },

          // error: function (file, response) {
              // file.previewElement.classList.add("dz-error");
          // }
      // });
  // }); 

// })(jQuery);

/* ToTop
 ========================================================*/
;
(function ($) {
    var o = $('html');
    if (o.hasClass('desktop')) {
        include('js/jquery.ui.totop.js');

        $(document).ready(function () {
            $().UItoTop({
                easingType: 'easeOutQuart',
                containerClass: 'toTop fa fa-angle-up'
            });
        });
    }
})(jQuery);

/* Tooltip & Modal Dialog
 ========================================================*/
;
(function ($) {
    include('js/jquery-ui.js');

    var o = $("[data-tooltip]");

    if (o.length > 0) {
        $(document).ready(function () {
           $("[data-tooltip]").tooltip({
                track: true,
                position: {
                    my: "center+10 bottom-10",
                    at: "center top"
                }
            });
        });
    }

     var v = $('.modal-win');

     if (o.length > 0) {
         $( "#create-user" ).button().on( "click", function() {
           dialog.dialog( "open" );
         });
     }

})(jQuery);




/* Dropdown
 ========================================================*/
;
(function ($) {
    $(document).ready(function () {
        $('.hide_link').on('click', function(){
            $(this).parents('.dropdown_on').slideUp(function (){
                $(this).siblings('.dropdown_off').fadeIn('slow');
            })
            return false;
        });
        $('.show_link').on('click', function(){
            $(this).parents('.dropdown_off').hide().siblings('.dropdown_on').slideDown();
            return false;
        })
    });
})(jQuery);

/* FancyBox
========================================================*/
// ;(function ($) {
    // include('js/jquery.fancybox.js');
    // $(document).ready(function () {
        // $(".open-modal").fancybox({
            // helpers : {
                    // title : null
                // }
        // });
    // });
// })(jQuery);

/* EqualHeights
 ========================================================*/
;
(function ($) {
    var o = $('[data-equal-group]');
    if (o.length > 0) {
        include('js/jquery.equalheights.js');
    }
})(jQuery); 

/* Copyright Year
 ========================================================*/
;
(function ($) {
    var currentYear = (new Date).getFullYear();
    $(document).ready(function () {
        $("#copyright-year").text((new Date).getFullYear());
    });
})(jQuery);


/* Superfish menus
 ========================================================*/
;
(function ($) {
    include('js/superfish.js');
})(jQuery);

/* Navbar
 ========================================================*/
;
(function ($) {
    include('js/jquery.rd-navbar.js');
})(jQuery);


/* Google Map
 ========================================================*/
// ;
// (function ($) {

    // var o = document.getElementById("google-map");
    // if (o) {

    // include('http://maps.google.ru/maps/api/js?sensor=false&amp;language=ru');
        
    // $(document).ready(function () {

        // // Map style
        // var customMapType = new google.maps.StyledMapType(
            // [{"featureType":"landscape","stylers":[{"saturation":-100},{"lightness":60}]},{"featureType":"road.local","stylers":[{"saturation":-100},{"lightness":40},{"visibility":"on"}]},{"featureType":"transit","stylers":[{"saturation":-100},{"visibility":"simplified"}]},{"featureType":"administrative.province","stylers":[{"visibility":"off"}]},{"featureType":"water","stylers":[{"visibility":"on"},{"lightness":30}]},{"featureType":"road.highway","elementType":"geometry.fill","stylers":[{"color":"#ef8c25"},{"lightness":40}]},{"featureType":"road.highway","elementType":"geometry.stroke","stylers":[{"visibility":"off"}]},{"featureType":"poi.park","elementType":"geometry.fill","stylers":[{"color":"#b6c54c"},{"lightness":40},{"saturation":-40}]},{}], 
            // {
              // name: 'Custom Style'
            // });
        // var customMapTypeId = 'custom_style';


        // // Main map on the index.html

            // var x = document.getElementById("demo");

            // function getLocation() {
                  // if (navigator.geolocation) {
                      // navigator.geolocation.getCurrentPosition(showPosition, showError);
                  // } else { 
                      // x.innerHTML = "Geolocation is not supported by this browser.";
                  // }
              // }

            // function showPosition(position) {
                // lat = position.coords.latitude;
                // lon = position.coords.longitude;
                // latlon = new google.maps.LatLng(lat, lon);
                // mapholder = o;
                

                // var myOptions = {
                    // center:latlon,
                    // zoom: 15,
                    // scrollwheel: false,
                    // mapTypeControlOptions: {
                        // mapTypeIds: [google.maps.MapTypeId.ROADMAP, customMapTypeId]
                    // }
                // }
                  
                // var map = new google.maps.Map(o, myOptions);

                // var marker = new google.maps.Marker({
                    // position:latlon,
                    // map:map,
                    // icon: 'images/gmap_marker.png',
                    // title:"Вы находитесь здесь!"
                // });


                    // //Маркер Ресторана №2 для примера

                    // var marker2 = new google.maps.Marker({
                    // position:{lat: 46.48, lng: 30.752},
                    // map:map,
                    // icon: 'images/gmap_marker_rest.png',
                    // title:'Ресторан "У моря"'});

                    // var contentString2 = '<div>' +
                    // '<div id="bodyContent">' +
                    // '<p><a href="review.html">Ресторан "У моря"</a></p>' + 
                    // '<p><span class="star-rating star-rating4"><span class="fa fa-star"></span><span class="fa fa-star"></span><span class="fa fa-star"></span><span class="fa fa-star"></span><span class="fa fa-star"></span></span></p>' +                    
                    // '</div>' +
                    // '</div>';

                    // var infowindow = new google.maps.InfoWindow({
                        // content: contentString2
                    // });
                    // google.maps.event.addListener(marker2, 'click', function() {
                        // infowindow.open(map, marker2);
                    // });


                // map.mapTypes.set(customMapTypeId, customMapType);
                // map.setMapTypeId(customMapTypeId);
            // }

            // function showError(error) {
                  // switch(error.code) {
                      // case error.PERMISSION_DENIED:
                          // x.innerHTML = "User denied the request for Geolocation."
                          // break;
                      // case error.POSITION_UNAVAILABLE:
                          // x.innerHTML = "Location information is unavailable."
                          // break;
                      // case error.TIMEOUT:
                          // x.innerHTML = "The request to get user location timed out."
                          // break;
                      // case error.UNKNOWN_ERROR:
                          // x.innerHTML = "An unknown error occurred."
                          // break;
                  // }
            // }

            // getLocation();
        // })
    // };

    // var o2 = document.getElementById("google-map-sm");
    // if (o2) {

    // include('http://maps.google.ru/maps/api/js?sensor=false&amp;language=ru');
        
    // $(document).ready(function () {

        // // Map style
        // var customMapType = new google.maps.StyledMapType(
            // [{"featureType":"landscape","stylers":[{"saturation":-100},{"lightness":60}]},{"featureType":"road.local","stylers":[{"saturation":-100},{"lightness":40},{"visibility":"on"}]},{"featureType":"transit","stylers":[{"saturation":-100},{"visibility":"simplified"}]},{"featureType":"administrative.province","stylers":[{"visibility":"off"}]},{"featureType":"water","stylers":[{"visibility":"on"},{"lightness":30}]},{"featureType":"road.highway","elementType":"geometry.fill","stylers":[{"color":"#ef8c25"},{"lightness":40}]},{"featureType":"road.highway","elementType":"geometry.stroke","stylers":[{"visibility":"off"}]},{"featureType":"poi.park","elementType":"geometry.fill","stylers":[{"color":"#b6c54c"},{"lightness":40},{"saturation":-40}]},{}], 
            // {
              // name: 'Custom Style'
            // });
        // var customMapTypeId = 'custom_style';


        // // small map on the review page (rewiew.html)


            // var lat = 46.48; //сюда передаются координаты из БД
            // var lon = 30.752; //сюда передаются координаты из БД
            // var latlon = new google.maps.LatLng(lat, lon);

            // var myOptions = {
                // center: latlon,
                // zoom: 15,
                // scrollwheel: false,
                // mapTypeControlOptions: {
                    // mapTypeIds: [google.maps.MapTypeId.ROADMAP, customMapTypeId]
                // }
            // }
            
            // var map = new google.maps.Map(o2, myOptions);

            // var marker = new google.maps.Marker({
                // position: latlon,
                // map:map,
                // icon: 'images/gmap_marker_rest.png',
                // title:""
            // });

            // var contentString = '<div>' +
            // '<div id="bodyContent">' +
            // '<p class="w-name">Ресторан "У моря"</p>' +  
            // '<p>Одесса, ул.Ресторанная, 32/56</p>' +
            // '</div>' +
            // '</div>';

            // var infowindow = new google.maps.InfoWindow({
                // content: contentString
            // });

            // infowindow.open(map, marker);

            // map.mapTypes.set(customMapTypeId, customMapType);
            // map.setMapTypeId(customMapTypeId);
            
        // });
    // };

    

// })(jQuery);

/* WOW
 ========================================================*/
;
(function ($) {
    var o = $('html');

    if ((navigator.userAgent.toLowerCase().indexOf('msie') == -1 ) || (isIE() && isIE() > 9)) {
        if (o.hasClass('desktop')) {
            include('js/wow.js');

            $(document).ready(function () {
                new WOW().init();
            });
        }
    }
})(jQuery);

/* Responsive Tabs
 ========================================================*/
;
(function ($) {
    var o = $('.resp-tabs');
    if (o.length > 0) {
        include('js/jquery.responsive.tabs.js');

        $(document).ready(function () {
            o.easyResponsiveTabs();
        });
    }
})(jQuery);


/* Orientation tablet fix
 ========================================================*/
$(function () {
    // IPad/IPhone
    var viewportmeta = document.querySelector && document.querySelector('meta[name="viewport"]'),
        ua = navigator.userAgent,

        gestureStart = function () {
            viewportmeta.content = "width=device-width, minimum-scale=0.25, maximum-scale=1.6, initial-scale=1.0";
        },

        scaleFix = function () {
            if (viewportmeta && /iPhone|iPad/.test(ua) && !/Opera Mini/.test(ua)) {
                viewportmeta.content = "width=device-width, minimum-scale=1.0, maximum-scale=1.0";
                document.addEventListener("gesturestart", gestureStart, false);
            }
        };

    scaleFix();
    // Menu Android
    if (window.orientation != undefined) {
        var regM = /ipod|ipad|iphone/gi,
            result = ua.match(regM);
        if (!result) {
            $('.sf-menus li').each(function () {
                if ($(">ul", this)[0]) {
                    $(">a", this).toggle(
                        function () {
                            return false;
                        },
                        function () {
                            window.location.href = $(this).attr("href");
                        }
                    );
                }
            })
        }
    }
});
var ua = navigator.userAgent.toLocaleLowerCase(),
    regV = /ipod|ipad|iphone/gi,
    result = ua.match(regV),
    userScale = "";
if (!result) {
    userScale = ",user-scalable=0"
}
document.write('<meta name="viewport" content="width=device-width,initial-scale=1.0' + userScale + '">');
