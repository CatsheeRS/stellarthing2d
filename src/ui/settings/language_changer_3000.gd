extends OptionButton

func _ready() -> void:
	var g = TranslationServer.get_locale()
	if g.begins_with("en"): selected = 0
	if g.begins_with("pt"): selected = 1

func on_item_selected(index: int) -> void:
	if index == 0: TranslationServer.set_locale("en")
	if index == 1: TranslationServer.set_locale("pt")
