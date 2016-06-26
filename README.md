This project consists in making the realization of a musical fountain commanded by a
computer through a card STM32F4-DISCOVERY.
The user Connects the Serial cable to the computer and to the STM32,
then starts the application by choosing the COMx port (the serial port)
the user will be able to start playing music.
The application makes the fast Fourier transform (FFT) and sends the spectrum level 
and the frequency to STM32.
STM32 receives the Commands (DATA) from the Application on PC, and applies those commands 
upon the Vumeter (Based on LEDs) and the water electropumps, such a way that the level of Water 
will follow the variability of the Spectrum level.