using System;

namespace Foundation
{
    //��� ������������� ������
    [Serializable]
    public struct LocalizedString
    {
        public string LocalizationID;

        public LocalizedString(string id)
        {
            LocalizationID = id;
        }
    }
}
