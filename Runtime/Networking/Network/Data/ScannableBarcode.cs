using System;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking {
    [Serializable]
    public abstract class ScannableBarcode {
        public string barcode;
        public string Barcode {
            get => barcode; set=>barcode = value;
        }
        public abstract object AssetKey { get; set; }
    }

    [Serializable]
    public class ScannableBarcodeT<T> : ScannableBarcode {
        public T Asset;
        public override object AssetKey {
            get => Asset;
            set => Asset = (T)value;
        }
    }
}
