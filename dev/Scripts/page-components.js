//definition of basic bricks
	components.blocItem = function(id, on, type, title, attr, dataAttr){
			var self = this;
			self.id = id;
			self.type = type;
			self.on = ko.observable(on || false);
			self.title = translator.translate(title);
			self.attr = attr || {};
			self.dataAttr = dataAttr || {};
	};
	components.bloc = function(id, on, type, title, items){
			var self = this;
			self.id = id;
			self.type = type;
			self.on = ko.observable(on || false);
			self.title = translator.translate(title);
			self.items = items || [];
	};
	components.sideBar = function(id, on, items){
			var self = this;
			self.id = id;
			self.on = ko.observable(on || false);
			self.items = items || [];
	};
	components.section = function(id, on, title, body){
			var self = this;
			self.id = id;
			self.on = ko.observable(on || false);
			self.title = translator.translate(title);
			self.body = translator.translate(body);
	};
	components.sections = function(items){
			var self = this;
			self.items = items || [];
	};
	components.page = function(param){
			var self = this;
			self.id = param.id;
			self.on = ko.observable(param.on || false);
			self.leftSideBar = param.leftSideBar || {on : false, items : []};
			self.rightSideBar = param.rightSideBar || {on : false, items : []};;
			self.title = translator.translate(param.title);
			self.sections = param.sections || [];
	};
