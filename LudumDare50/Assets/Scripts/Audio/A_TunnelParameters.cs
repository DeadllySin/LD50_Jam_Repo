using UnityEngine;

public class A_TunnelParameters : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.am.reverbTunnelSSInstance.start();
            AudioManager.am.tunnelOcclusionSSInstance.start();
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("TRX_Tunnel", 1);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.am.reverbTunnelSSInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            AudioManager.am.tunnelOcclusionSSInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("TRX_Tunnel", 0);
        }
    }
}
