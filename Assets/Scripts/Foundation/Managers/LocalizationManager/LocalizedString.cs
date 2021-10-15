using System;

namespace Foundation
{
    //Это идентификатор строки
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
