
(function($) {

   'use strict'/*------------------------------------------------------------------------------*/
/* Preloader
/*------------------------------------------------------------------------------*/
// makes sure the whole site is loaded
 $(window).on("load",function() {
        // will first fade out the loading animation
     $("#preloader").fadeOut();
        // will fade out the whole DIV that covers the website.
     $("#status").fadeOut(9000);
});

/*------------------------------------------------------------------------------*/
/* Fixed-header
/*------------------------------------------------------------------------------*/$(window).scroll(function(){
    if ( matchMedia( 'only screen and (min-width: 1200px)' ).matches ) 
    {
        if ($(window).scrollTop() >= 50 ) {
            $('.ttm-stickable-header').addClass('fixed-header');
            $('.ttm-stickable-header').addClass('visible-title');
        }
        else {

            $('.ttm-stickable-header').removeClass('fixed-header');
            $('ttm-stickable-header').removeClass('visible-title');
            }
    }
});

/*------------------------------------------------------------------------------*/
/* Menu
/*------------------------------------------------------------------------------*/

    $('ul li:has(ul)').addClass('has-submenu');
    $('ul li ul').addClass('sub-menu');    $("ul.dropdown li").on({

        mouseover: function(){
           $(this).addClass("hover");
        },  
        mouseout: function(){
           $(this).removeClass("hover");
        }, 

    });
    
    var $menu = $('#menu'), $menulink = $('#menu-toggle-form'), $menuTrigger = $('.has-submenu > a');
    $menulink.on('click',function (e) {

        $menulink.toggleClass('active');
        $menu.toggleClass('active');
    });

    $menuTrigger.on('click',function (e) {
        e.preventDefault();
        var t = $(this);
        t.toggleClass('active').next('ul').toggleClass('active');
    });

    $('ul li:has(ul)');
	
/*------------------------------------------------------------------------------*/
/* Animation on scroll: Number rotator
/*------------------------------------------------------------------------------*/
    
    $("[data-appear-animation]").each(function() {
        var self      = $(this);
        var animation = self.data("appear-animation");
        var delay     = (self.data("appear-animation-delay") ? self.data("appear-animation-delay") : 0);
        
        if( $(window).width() > 959 ) {
            self.html('0');
            self.waypoint(function(direction) {
                if( !self.hasClass('completed') ){
                    var from     = self.data('from');
                    var to       = self.data('to');
                    var interval = self.data('interval');
                    self.numinate({
                        format: '%counter%',
                        from: from,
                        to: to,
                        runningInterval: 2000,
                        stepUnit: interval,
                        onComplete: function(elem) {
                            self.addClass('completed');
                        }
                    });
                }
            }, { offset:'85%' });
        } else {
            if( animation == 'animateWidth' ) {
                self.css('width', self.data("width"));
            }
        }
    });
	
/*------------------------------------------------------------------------------*/
/* Tab
/*------------------------------------------------------------------------------*/ 

$('.ttm-tabs').each(function() {
    $(this).children('.content-tab').children().hide();
    $(this).children('.content-tab').children().first().show();
    $(this).find('.tabs').children('li').on('click', function(e) {  
        var liActive = $(this).index(),
            contentActive = $(this).siblings().removeClass('active').parents('.ttm-tabs').children('.content-tab').children().eq(liActive);
        contentActive.addClass('active').fadeIn('slow');
        contentActive.siblings().removeClass('active');
        $(this).addClass('active').parents('.ttm-tabs').children('.content-tab').children().eq(liActive).siblings().hide();
        e.preventDefault();
    });
});

/*------------------------------------------------------------------------------*/
/* Accordion
/*------------------------------------------------------------------------------*/

/*https://www.antimath.info/jquery/quick-and-simple-jquery-accordion/*/
$(".accordion").each(function(){

    var allPanels = $('.toggle').children(".toggle-content").hide();
    $('.toggle').children(".toggle-content").eq(1).slideDown("easeOutExpo");
    $('.toggle').children(".toggle-title").children("a").eq(1).addClass("active");

    $('.toggle').children(".toggle-title").children("a").on('click', function(){        
        var current = $(this).parent().next(".toggle-content");
        $(".toggle-title > a").removeClass("active");
        $(this).addClass("active");
        allPanels.not(current).slideUp("easeInExpo");
        $(this).parent().next().slideDown("easeOutExpo");                
        return false;                
    });

});

/*------------------------------------------------------------------------------*/
/* Isotope
/*------------------------------------------------------------------------------*/

$(window).on('load', function() {

    var $container = $('#isotopeContainer');

    // filter items when filter link is clicked
    $('#filters a').on('click', function(){
      var selector = $(this).attr('data-filter');
      $container.isotope({ filter: selector });
      return false;
    });

    var $optionSets = $('#filters li'),
        $optionLinks = $optionSets.find('a');

        $optionLinks.on('click', function(){
            var $this = $(this);
            // don't proceed if already selected
            if ( $this.hasClass('selected') ) {
              return false;
            }
            var $optionSet = $this.parents('#filters');
            $optionSet.find('.selected').removeClass('selected');
            $this.addClass('selected');

            // make option object dynamically, i.e. { filter: '.my-filter-class' }
            var options = {},
                key = $optionSet.attr('data-option-key'),
                value = $this.attr('data-option-value');
            // parse 'false' as false boolean
            value = value === 'false' ? false : value;
            options[ key ] = value;
            if ( key === 'layoutMode' && typeof changeLayoutMode === 'function' ) {
              // changes in layout modes need extra logic
              changeLayoutMode( $this, options )
            } else {
              // otherwise, apply new options
              $container.isotope( options );
            }

            return false;
        });
   });
  
/*------------------------------------------------------------------------------*/
/* Prettyphoto
/*------------------------------------------------------------------------------*/
jQuery(document).ready(function(){

 // Normal link
jQuery('a[href*=".jpg"], a[href*=".jpeg"], a[href*=".png"], a[href*=".gif"]').each(function(){
    if( jQuery(this).attr('target')!='_blank' && !jQuery(this).hasClass('prettyphoto') && !jQuery(this).hasClass('modula-lightbox') ){
        var attr = $(this).attr('data-gal');
        if (typeof attr !== typeof undefined && attr !== false && attr!='prettyPhoto' ) {
            jQuery(this).attr('data-rel','prettyPhoto');
        }
    }
});     jQuery('a[data-gal^="prettyPhoto"]').prettyPhoto();
jQuery('a.ttm_prettyphoto').prettyPhoto();
jQuery('a[data-gal^="prettyPhoto"]').prettyPhoto();
jQuery("a[data-gal^='prettyPhoto']").prettyPhoto({hook: 'data-gal'})

});
    
/*------------------------------------------------------------------------------*/
/* owlCarousel
/*------------------------------------------------------------------------------*/

	// =====1: main slide ==== 
	$(".main-slide").owlCarousel({  
        loop:true,
        margin:0,
        nav: $('.main-slide').data('nav'),
        dots: $('.main-slide').data('dots'),                     
        autoplay: $('.main-slide').data('auto'),
        smartSpeed: 3000,
        responsive:{
            0:{
                items:1
            },
            576:{
                items:1
            },
            768:{
                items:1
            },
            992:{
                items: 1
            }
        }
    });
	
	// =====1: affiliate shopping slide ==== 
    $(".affiliate-shopping-slide").owlCarousel({  
        loop:true,
        margin:0,
        nav: $('.affiliate-shopping-slide').data('nav'),
        dots: $('.affiliate-shopping-slide').data('dots'),                     
        autoplay: $('.affiliate-shopping-slide').data('auto'),
        smartSpeed: 3000,
        responsive:{
            0:{
                items:1
            },
            576:{
                items:2
            },
            768:{
                items:3
            },
            992:{
                items: 4
            }
        }
    });
	
	// =====1: affiliate shopping slide ==== 
	$(".team-slide").owlCarousel({  
        loop:true,
        margin:0,
        nav: true,
        dots: false,                     
        autoplay: true,
        smartSpeed: 800,
        responsive:{
            0:{
                items:1,
            },
            480:{
                items:2,
            },
            768:{
                items:3
            },
            1200:{
                items: 4
            }
        }
    });
	
	// =====3- Testimonial slide ==== 
    $(".testimonial-slide").owlCarousel({  
        loop:true,
        margin:0,
        smartSpeed: 1000,
        nav: $('.testimonial-slide').data('nav'),
        dots: $('.testimonial-slide').data('dots'), 
        autoplay: $('.testimonial-slide').data('auto'),
        responsive:{
            0:{
                items:1,
            },
            600:{
                items:1,
            },
            1000:{
                items: $('.testimonial-slide').data('item')
            }
        }
    });


    // =====2: Icons slide ==== 
    $(".icons-slide").owlCarousel({
        loop: true,
        margin: 0,
        nav: true,
        dots: false,
        autoplay: true,
        smartSpeed: 800,
        responsive: {
            0: {
                items: 2,
            },
            480: {
                items: 3,
            },
            768: {
                items: 4
            },
            1200: {
                items: 6
            }
        }
    });

/*------------------------------------------------------------------------------*/
/* Back to top
/*------------------------------------------------------------------------------*/

// ===== Scroll to Top ==== 
jQuery('#totop').hide();
jQuery(window).scroll(function() {
    "use strict";
    if (jQuery(this).scrollTop() >= 100) {        // If page is scrolled more than 50px
        jQuery('#totop').fadeIn(200);    // Fade in the arrow
        jQuery('#totop').addClass('top-visible');
    } else {
        jQuery('#totop').fadeOut(200);   // Else fade out the arrow
        jQuery('#totop').removeClass('top-visible');
    }
});
jQuery('#totop').on('click', function() {      // When arrow is clicked
    jQuery('body,html').animate({
        scrollTop : 0                       // Scroll to top of body
    }, 500);
    return false;
}); $(function() {
    
    });
})(jQuery);




