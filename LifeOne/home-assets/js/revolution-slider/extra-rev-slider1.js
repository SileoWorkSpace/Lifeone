(function($) {
  "use strict";

  if(typeof revslider_showDoubleJqueryError === "undefined") {
    function revslider_showDoubleJqueryError(sliderID) {
      var err = "<div class='rs_error_message_box'>";
      err += "<div class='rs_error_message_oops'>Oops...</div>";
      err += "<div class='rs_error_message_content'>";
      err += "You have some jquery.js library include that comes after the Slider Revolution files js inclusion.<br>";
      err += "To fix this, you can:<br>&nbsp;&nbsp;&nbsp; 1. Set 'Module General Options' -> 'Advanced' -> 'jQuery & OutPut Filters' -> 'Put JS to Body' to on";
      err += "<br>&nbsp;&nbsp;&nbsp; 2. Find the double jQuery.js inclusion and remove it";
      err += "</div>";
    err += "</div>";
      var slider = document.getElementById(sliderID); slider.innerHTML = err; slider.style.display = "block";
    }
  }

  var revapi13,
  tpj;
  jQuery(function() {
    tpj = jQuery;
    revapi13 = tpj("#rev_slider_1_1");
    if (revapi13 == undefined || revapi13.revolution == undefined) {
      revslider_showDoubleJqueryError("rev_slider_1_1");
    } else {
      revapi13.revolution({
        sliderLayout: "fullwidth",
        visibilityLevels: "1240,1024,778,480",
        gridwidth: "1310,1024,778,480",
        gridheight: "800,700,640,600",
        spinner: "spinner0",
        perspective: 600,
        perspectiveType: "local",
        editorheight: "800,700,640,600",
        responsiveLevels: "1240,1024,778,480",
        progressBar: {
          color: "rgba(199,199,199,0.5)",
          size: 5,
          x: 0,
          y: 0
        },
        navigation: {
          onHoverStop: false,
          arrows: {
            enable: true,
            tmp: "<div class=\"tp-title-wrap\">   <div class=\"tp-arr-imgholder\"></div> </div>",
            style: "zeus",
            hide_onmobile: true,
            hide_under: "778px",
            left: {
              h_offset: 30
            },
            right: {
              h_offset: 30
            }
          },
          bullets: {
            enable: true,
            tmp: "",
            style: "hermes",
            hide_onmobile: true,
            hide_under: "1024px",
            hide_onleave: true,
            h_align: "left",
            h_offset: 60,
            v_offset: 80,
            container: "layergrid"
          }
        },
        parallax: {
          levels: [2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 30],
          type: "mouse",
          origo: "slidercenter",
          speed: "300ms",
          speedbg: "3000ms",
          speedls: "3000ms"
        },
        scrolleffect: {
          set: true,
          multiplicator: 1.3,
          multiplicator_layers: 1.3
        },
        sbtimeline: {
          set: true
        },
        fallbacks: {
          allowHTML5AutoPlayOnAndroid: true
        },
      });
    }

  });

})(jQuery);