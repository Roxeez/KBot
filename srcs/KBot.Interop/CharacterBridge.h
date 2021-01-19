#pragma once
#include "Character.h"

namespace KBot
{
	namespace Interop
	{
		public ref class CharacterBridge
		{
		public:
            void Walk(short x, short y);
            short GetPositionX();
            short GetPositionY();
		};
	};
};

