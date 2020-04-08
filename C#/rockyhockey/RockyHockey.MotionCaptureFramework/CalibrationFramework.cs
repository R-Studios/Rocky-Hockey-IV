using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    public class CalibrationFramework
    {
        private GameFieldPosition gameFieldPositionAsReference = new GameFieldPosition();
        private GameFieldPosition lowerLeftGameFieldPosition = new GameFieldPosition();
        private GameFieldPosition lowerRightGameFieldPosition = new GameFieldPosition();
        private const double Tolerance = 0;
        private bool result;

        //public void CalibrateGameField()
        //{
        //    var lowerLeftGameFieldPosition = GetGameFieldPosition();
        //    var gameFieldPosition = GetGameFieldPosition();
        //}

//        private GameFieldPosition GetGameFieldPosition()
//        {
//            result = false;
//            while (!result)
//            {
//                //var cameraPictureQueue = CameraPictureQueue.GetInstance().GetElementsFromQueue();
//                //foreach (var current in cameraPictureQueue)
//                //{
//                //    if (current is KeyValuePair<GameFieldPosition, int> keyValuePair)
//                //    {
//                //        CheckIfPositionsAreTheSame(keyValuePair);
//                //    }
//                }
//            }

//            return gameFieldPositionAsReference;
//        }


//        private void CheckIfPositionsAreTheSame(KeyValuePair<GameFieldPosition, int> keyValuePair)
//        {
//            var gameFieldPosition = keyValuePair.Key;
//            IsReferencePositionSet(gameFieldPosition);
//            if (Math.Abs(gameFieldPosition.X - gameFieldPositionAsReference.X) > Tolerance ||
//                Math.Abs(gameFieldPosition.Y - gameFieldPositionAsReference.Y) > Tolerance)
//            {
//                result = false;
//            }
//            else
//            {
//                result = true;
//            }
//        }

//        private void IsReferencePositionSet(GameFieldPosition gameFieldPosition)
//        {
//            if (Math.Abs(gameFieldPositionAsReference.X) > Tolerance &&
//                Math.Abs(gameFieldPositionAsReference.Y) > Tolerance)
//            {
//                gameFieldPositionAsReference = gameFieldPosition;
//            }

//        }
//    }
    }
}