using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ducksten {
    public static class FloatExtensions {
        public static bool IsApprox(this float value, float approxValue) {
            return Mathf.Approximately(value, approxValue);
        }

        public static bool IsApproxZero(this float value) {
            return IsApprox(value, 0f);
        }
    }
}

