using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoundCameraPos : CinemachineExtension
{
    public float PPU = 32;
    // Start is called before the first frame update
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if(stage == CinemachineCore.Stage.Body)
        {
            Vector3 pos = state.FinalPosition;

            Vector3 pos2 = new Vector3(Round(pos.x), Round(pos.y), pos.z);

            state.PositionCorrection += pos2 - pos;
        }
    }

    float Round(float x){
        return Mathf.Round(x * PPU) / PPU;
    }
}
