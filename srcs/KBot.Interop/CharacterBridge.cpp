#include "CharacterBridge.h"
#include "UnmanagedCharacter.h"

namespace KBot
{
	namespace Interop
	{
		void CharacterBridge::Walk(short x, short y)
		{
			UnmanagedCharacter::GetInstance()->Walk(x, y);
		}
	};
};