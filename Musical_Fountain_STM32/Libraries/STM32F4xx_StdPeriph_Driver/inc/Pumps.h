#ifndef __Pumps_H
#define __Pumps_H
#include "stm32f4xx.h"
#include "stm32f4xx_tim.h"
#define EPs_GPIO_PORT   GPIOB
#define EPs_DATA_PIN    GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_4 							           
#define EPs_GPIO_CLK    RCC_AHB1Periph_GPIOB

void ElectroPumps_Init();

void ElectroPumps_Handler(uint8_t Frequency,uint16_t level);
#endif