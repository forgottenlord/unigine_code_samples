using Unigine;

[Component(PropertyGuid = "20cf275447d36aa388934fa6a4dc22cdb77f65c8")]
public class TransformEulerAngles : Component
{
	public enum COMPOSITION_TYPE
	{
		XYZ = 0,
		XZY,
		YXZ,
		YZX,
		ZXY,
		ZYX,
		COUNT
	}

	public vec3 EulerAngles { get; private set; } = vec3.ZERO;
	public vec3 DecompositionAngles { get; private set; } = vec3.ZERO;

	public COMPOSITION_TYPE compositionType { get; private set; } = COMPOSITION_TYPE.XYZ;
	public COMPOSITION_TYPE decompositionType { get; private set; } = COMPOSITION_TYPE.XYZ;

	private void Init()
	{
		// subscribe on event from ui controls
		TransformEulerAnglesUI.anglesChanged += OnAnglesChanged;
		TransformEulerAnglesUI.compositionTypeChanged += OnCompositionTypeChanged;
		TransformEulerAnglesUI.decompositionTypeChanged += OnDecompositionTypeChanged;
	}

	private void Shutdown()
	{
		// unsubscribe on event from ui controls
		TransformEulerAnglesUI.anglesChanged -= OnAnglesChanged;
		TransformEulerAnglesUI.compositionTypeChanged -= OnCompositionTypeChanged;
		TransformEulerAnglesUI.decompositionTypeChanged -= OnDecompositionTypeChanged;
	}

	private void OnAnglesChanged(vec3 angles)
	{
		EulerAngles = angles;
		UpdateRotation();
		UpdateDecompositionAngles();
	}

	private void OnCompositionTypeChanged(COMPOSITION_TYPE type)
	{
		compositionType = type;
		UpdateRotation();
	}

	private void OnDecompositionTypeChanged(COMPOSITION_TYPE type)
	{
		decompositionType = type;
		UpdateDecompositionAngles();
	}

	private void UpdateRotation()
	{
		// get new rotation based on current composition type
		mat4 rot = mat4.IDENTITY;
		switch (compositionType)
		{
			case COMPOSITION_TYPE.XYZ: rot = MathLib.ComposeRotationXYZ(EulerAngles); break;
			case COMPOSITION_TYPE.XZY: rot = MathLib.ComposeRotationXZY(EulerAngles); break;
			case COMPOSITION_TYPE.YXZ: rot = MathLib.ComposeRotationYXZ(EulerAngles); break;
			case COMPOSITION_TYPE.YZX: rot = MathLib.ComposeRotationYZX(EulerAngles); break;
			case COMPOSITION_TYPE.ZXY: rot = MathLib.ComposeRotationZXY(EulerAngles); break;
			case COMPOSITION_TYPE.ZYX: rot = MathLib.ComposeRotationZYX(EulerAngles); break;
		}

		node.SetWorldRotation(new quat(rot));
	}

	private void UpdateDecompositionAngles()
	{
		// update decomposition angles based on current decomposition type
		mat3 rot = node.GetWorldRotation().Mat3;
		switch (decompositionType)
		{
			case COMPOSITION_TYPE.XYZ: DecompositionAngles = MathLib.DecomposeRotationXYZ(rot); break;
			case COMPOSITION_TYPE.XZY: DecompositionAngles = MathLib.DecomposeRotationXZY(rot); break;
			case COMPOSITION_TYPE.YXZ: DecompositionAngles = MathLib.DecomposeRotationYXZ(rot); break;
			case COMPOSITION_TYPE.YZX: DecompositionAngles = MathLib.DecomposeRotationYZX(rot); break;
			case COMPOSITION_TYPE.ZXY: DecompositionAngles = MathLib.DecomposeRotationZXY(rot); break;
			case COMPOSITION_TYPE.ZYX: DecompositionAngles = MathLib.DecomposeRotationZYX(rot); break;
		}
	}
}
