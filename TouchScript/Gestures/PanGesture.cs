﻿/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using System.Collections.Generic;
using TouchScript.Clusters;
using TouchScript.Gestures.Simple;
using UnityEngine;

namespace TouchScript.Gestures
{
    /// <summary>
    /// Recognizes dragging gesture.
    /// </summary>
    [AddComponentMenu("TouchScript/Gestures/Pan Gesture")]
    public class PanGesture : SimplePanGesture
    {
        #region Private variables

        private Clusters2 clusters = new Clusters2();

        #endregion

        #region Public methods

        /// <inheritdoc />
        public override Vector2 ScreenPosition
        {
            get
            {
                if (activeTouches.Count == 0) return TouchPoint.INVALID_POSITION;
                if (activeTouches.Count == 1) return activeTouches[0].Position;
                return (clusters.GetCenterPosition(Clusters2.CLUSTER1) + clusters.GetCenterPosition(Clusters2.CLUSTER2))*.5f;
            }
        }

        /// <inheritdoc />
        public override Vector2 PreviousScreenPosition
        {
            get
            {
                if (activeTouches.Count == 0) return TouchPoint.INVALID_POSITION;
                if (activeTouches.Count == 1) return activeTouches[0].PreviousPosition;
                return (clusters.GetPreviousCenterPosition(Clusters2.CLUSTER1) + clusters.GetPreviousCenterPosition(Clusters2.CLUSTER2))*.5f;
            }
        }

        #endregion

        #region Unity methods

        #endregion

        #region Gesture callbacks

        /// <inheritdoc />
        protected override void touchesBegan(IList<TouchPoint> touches)
        {
            clusters.AddPoints(touches);

            base.touchesBegan(touches);
        }

        /// <inheritdoc />
        protected override void touchesMoved(IList<TouchPoint> touches)
        {
            clusters.Invalidate();

            base.touchesMoved(touches);
        }

        /// <inheritdoc />
        protected override void touchesEnded(IList<TouchPoint> touches)
        {
            clusters.RemovePoints(touches);

            base.touchesEnded(touches);
        }

        /// <inheritdoc />
        protected override void reset()
        {
            base.reset();

            clusters.RemoveAllPoints();
        }

        #endregion
    }
}