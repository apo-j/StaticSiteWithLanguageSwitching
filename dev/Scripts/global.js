var vars = Namespace('com.joe.vars');
var static = Namespace('com.joe.static');
var types = Namespace('com.joe.types');
var enums = Namespace('com.joe.enums');

//types
types.languages = {
	Chinese:'zh',
	French:'fr',
	English:'en'
}

//static
static.currentLanguage = 'currentLanguage';

//vars
vars.currentLanguage = types.languages.Chinese;
