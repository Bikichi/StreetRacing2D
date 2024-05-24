// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("bjWS0hnnf2aK8NbDvB1qdR+mDTWqKScoGKopIiqqKSkovi0N9ODqb8cUMujklmhDxwALqxIkqn/sSZbKYEGPPfqMTSHRJAuU8fzlC+V1SWHGd41Z7iIxyUX0agtU2R9Jw2eXwiTREMOnHxEKahJ5tDOEwZJxTvfU90FkLDfK74Oy/6E6tb/j1maIhxDNZmKBjgA5M7FDf7oXePaMi3OdYXWozERbPHv7YXczwAOfLFm6iD02Fk+yPju4d8k57wYap3L5rgquq6pDI6qtqjhs2tBn9tP/tVKDFxeAIxiqKQoYJS4hAq5grt8lKSkpLSgrXWP2kMC5isNQKMboqsuxl2uA3Mo7GSmg8faOUsqfbhY1v8emVoDOASEr23KxxPUwjSorKSgp");
        private static int[] order = new int[] { 8,1,6,9,9,10,13,9,11,12,10,11,12,13,14 };
        private static int key = 40;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
