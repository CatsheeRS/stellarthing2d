extends Button

@export var gggg: Node

func _pressed():
	Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
	UniverseManager.Save()
	UniverseManager.ResetUniverseButGdscript()
	get_tree().root.add_child(load("res://scenes/menu.tscn").instantiate())
	gggg.queue_free()
	get_node("/root/universe").queue_free()
	get_tree().paused = false
