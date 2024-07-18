extends Button

@export var gggg: Node

func _pressed():
	get_tree().root.add_child(load("res://scenes/menu.tscn").instantiate())
	gggg.queue_free()
	get_tree().paused = false
