extends Label

@export var slider: Slider
@export var prefix := ""
@export var suffix := ""

func _process(_delta: float) -> void:
	text = prefix + str(slider.value) + suffix
