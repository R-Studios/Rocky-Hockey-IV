using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    public interface ICalibrationForCameraProvider
    {
        /// <summary>
        /// Start calibrating the camera 
        /// </summary>
        Task StartCalibrationMode();

    }
}