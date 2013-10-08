ko.bindingHandlers.linkToSection = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		var value = ko.utils.unwrapObservable(valueAccessor());

		$(element).data('orgin', value);
    },
    update: function (element, valueAccessor) {
        var current = ko.utils.unwrapObservable(valueAccessor());
		var value = $(element).data('orgin');

        if (value !== current) {
		   $(element).css('outline', '1px solid red');
        }else{
			$(element).css('outline', '0');
		}
    }
};