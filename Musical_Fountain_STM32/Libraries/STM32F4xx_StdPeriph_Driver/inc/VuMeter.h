#ifndef __VuMeter_H
#define __VuMeter_H
/*----------------------------------------------------------------------------*/
                               /*Includes*/
#include "stm32f4xx.h"
/*----------------------------------------------------------------------------*/
                                /*VM1*/
#define VM1_GPIO_PORT   GPIOC
#define VM1_DATA_PIN    GPIO_Pin_1 | GPIO_Pin_2 | GPIO_Pin_3 | GPIO_Pin_4 | GPIO_Pin_5 | GPIO_Pin_6 | GPIO_Pin_7 |GPIO_Pin_8 							           
#define VM1_GPIO_CLK    RCC_AHB1Periph_GPIOC
                                /*VM2*/
#define VM2_GPIO_PORT   GPIOD
#define VM2_DATA_PIN    GPIO_Pin_7 | GPIO_Pin_8 | GPIO_Pin_9 | GPIO_Pin_10 | GPIO_Pin_11 | GPIO_Pin_12 | GPIO_Pin_13 | GPIO_Pin_14   
#define VM2_GPIO_CLK    RCC_AHB1Periph_GPIOD 
                                /*VM3*/
#define VM3_GPIO_PORT   GPIOE
#define VM3_DATA_PIN    GPIO_Pin_8 | GPIO_Pin_9 | GPIO_Pin_10 | GPIO_Pin_11 | GPIO_Pin_12 |GPIO_Pin_13 | GPIO_Pin_14 | GPIO_Pin_15       
#define VM3_GPIO_CLK    RCC_AHB1Periph_GPIOE
/*----------------------------------------------------------------------------*/
                          /*Functions Prototypes*/
void VuMeter_Init();
void VuMeter_Send_Data(uint16_t data,uint8_t line);
void VuMeter_Display(uint16_t level,uint8_t frequency);
#endif