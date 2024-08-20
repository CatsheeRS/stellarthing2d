extends Label

@export var slider: Slider
@export var prefix := ""
@export var suffix := ""
@export var multiplier := 1.0

func _process(_delta: float) -> void:
	text = prefix + str(slider.value * multiplier) + suffix
