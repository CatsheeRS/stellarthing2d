extends Button

@export var player: AudioStreamPlayer2D

func _pressed() -> void:
	player.play()
