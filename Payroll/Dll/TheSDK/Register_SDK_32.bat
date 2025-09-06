cd /d %~dp0
	echo system is x86
	copy .\*.dll %windir%\system32\
	regsvr32 %windir%\system32\zkemkeeper.dll
	
