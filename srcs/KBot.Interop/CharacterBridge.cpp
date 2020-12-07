#include "CharacterBridge.h"
#include "Character.h"

namespace KBot
{
	namespace Interop
	{
		void CharacterBridge::Walk(short x, short y)
		{
			Character::GetInstance()->Walk(x, y);
		}

        void CharacterBridge::PetWalk(short x, short y)
        {
            Character::GetInstance()->PetWalk(x, y);
        }
	};
};