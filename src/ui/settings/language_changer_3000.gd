extends OptionButton

func _ready() -> void:
	var config = ConfigFile.new()
	config.load("user://prefs.cfg")
	var he = config.get_value("locale", "language", OS.get_locale_language())
	TranslationServer.set_locale(he)
	config.save("user://prefs.cfg")
	
	var g = TranslationServer.get_locale()
	if g.begins_with("en"): selected = 0
	if g.begins_with("pt"): selected = 1

func on_item_selected(index: int) -> void:
	if index == 0: TranslationServer.set_locale("en")
	if index == 1: TranslationServer.set_locale("pt")
	
	var config = ConfigFile.new()
	config.load("user://prefs.cfg")
	config.set_value("locale", "language", TranslationServer.get_locale())
	config.save("user://prefs.cfg")
