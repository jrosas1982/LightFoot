// "required" support for Chosen; see https://github.com/harvesthq/chosen/issues/515, http://jsfiddle.net/hq7b426j/1/
$.fn.oldChosen = $.fn.chosen
$.fn.chosen = function (options) {
    var select = $(this),
        is_creating_chosen = !!options;

    if (is_creating_chosen && select.css('position') === 'absolute') {
        // if we are creating a chosen and the select already has the appropriate styles added
        // we remove those (so that the select hasn't got a crazy width), then create the chosen
        // then we re-add them later
        select.removeAttr('style');
    }

    var ret = select.oldChosen(options)

    // only act if the select has display: none, otherwise chosen is unsupported (iPhone, etc)
    if (is_creating_chosen && select.css('display') === 'none') {
        // https://github.com/harvesthq/chosen/issues/515#issuecomment-33214050
        // only do this if we are initializing chosen (no params, or object params) not calling a method
        select.attr('style', 'display:visible; position:absolute; clip:rect(0,0,0,0)');
        select.attr('tabindex', -1);
    }
    return ret
}