extends Button

@export var g: Control

func _toggled(toggled_on: bool) -> void:
	g.visible = toggled_on
