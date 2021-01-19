#include "PetBridge.h"

namespace KBot
{
    namespace Interop
    {
        void PetBridge::Walk(short x, short y)
        {
            Pet::GetInstance()->Walk(x, y);
        }
    }
}