extends Control

@export var thething: Control
@export var firthtab: Control

func _process(_delta: float) -> void:
	if visible:
		thething.visible = false
		visible = false
		firthtab.visible = true
