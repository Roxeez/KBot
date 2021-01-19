#include "CharacterBridge.h"

namespace KBot
{
	namespace Interop
	{
		void CharacterBridge::Walk(short x, short y)
		{
            Character::GetInstance()->Walk(x, y);
		}

        short CharacterBridge::GetPositionX()
        {
            return Character::GetInstance()->GetPositionX();
        }

        short CharacterBridge::GetPositionY()
        {
            return Character::GetInstance()->GetPositionY();
        }
	};
};