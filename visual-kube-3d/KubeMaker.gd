extends Node3D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	BuildKube()


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func BuildKube() -> void:
	var stool := SurfaceTool.new()
	stool.begin(Mesh.PRIMITIVE_TRIANGLES)
	
	stool.add_vertex(Vector3(-1,2,0))
	stool.add_vertex(Vector3(-1,2,0))
	stool.add_vertex(Vector3(1,2,0))
	stool.commit()
