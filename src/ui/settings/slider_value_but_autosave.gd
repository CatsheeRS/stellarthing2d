extends Label

# TODO: make funni display thing where it shows minutes when
# it's big and say "disabled" when it's at 0

@export var slider: Slider

func _process(_delta: float) -> void:
	if is_equal_approx(slider.value, 0):
		text = tr("Disabled")
		return
	
	var minutes = floori(slider.value / 60)
	if minutes == 0: text = str(slider.value) + "s"
	else:
		text = str(minutes) + "m " + str(slider.value - \
			(minutes * 60)) + "s"
