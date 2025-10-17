using System;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    [Serializable]
    public struct NetworkBarcodeT<T>
    {
        public string barcode;
        public T reference;
        public void SetReference(object _reference) {
            reference = (T)_reference;
        }
    }
}
