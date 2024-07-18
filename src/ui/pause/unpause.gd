extends Button

@export var b: Control
var lool = 0.0

func _pressed():
	hhjhhhhh()

func _process(delta):
	lool += delta
	if Input.is_action_just_pressed("pause") and lool > 0.1:
		lool = 0
		hhjhhhhh()

func hhjhhhhh():
	b.visible = false
	get_tree().paused = false

