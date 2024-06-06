// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("ugwpYXqHos7/sux3+PKumyvFyl045YEJFnE2tiw6fo1O0mEU98Vwew5u5+DndSGXnSq7nrL4H85aWs1uizrAFKNvfIQIuSdGGZRSBI4q2o8QLrvdjfTHjh1li6XnhvzaJs2Rh4pZf6Wp2yUOik1G5l9p5zKhBNuHLQzCcLfBAGycaUbZvLGoRqg4BCznZGplVedkb2fnZGRl82BAua2nIlXnZEdVaGNsT+Mt45JoZGRkYGVmWwL/c3b1OoR0oktX6j+040fj5udpnF2O6lJcRydfNPl+yYzfPAO6mSN4359UqjIrx72bjvFQJzhS60B4gCsvzMNNdH78DjL3WjW7wcY+0Cx2VGTtvLvDH4fSI1t48orrG82DTGxmlj/8ibh9wGdmZGVk");
        private static int[] order = new int[] { 13,11,5,3,7,6,9,11,13,12,10,13,13,13,14 };
        private static int key = 101;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
