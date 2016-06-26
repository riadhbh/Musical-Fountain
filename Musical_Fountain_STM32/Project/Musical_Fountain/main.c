#include <stdio.h>
#include "main.h"
#include "stm32f4xx.h"
#include "VuMeter.h"
#include "Pumps.h"
#include "stm32f4xx_usart.h"

/** @addtogroup STM32L1xx_StdPeriph_Examples
  * @{
  */

/** @addtogroup SysTick_Example
  * @{
  */ 

/* Private typedef -----------------------------------------------------------*/
GPIO_InitTypeDef GPIO_InitStructure; 
/* Private define ------------------------------------------------------------*/
/* Private macro -------------------------------------------------------------*/
/* Private variables ---------------------------------------------------------*/

uint8_t  data=0 ;
uint8_t lin=0,Lvl=0;
uint8_t SleepMode = 0;
/* Private function prototypes -----------------------------------------------*/
static __IO uint32_t TimingDelay;
EXTI_InitTypeDef   EXTI_InitStructure;
void Delay(__IO uint32_t nTime);
void USART2_Init(void);
#ifdef __GNUC__
  /* With GCC/RAISONANCE, small printf (option LD Linker->Libraries->Small printf
     set to 'Yes') calls __io_putchar() */
  #define PUTCHAR_PROTOTYPE int __io_putchar(int ch)
#else
  #define PUTCHAR_PROTOTYPE int fputc(int ch, FILE *f)
#endif /* __GNUC__ */
//uint8_t ser_scanf (USART_TypeDef* USARTx) ;
uint8_t USART2GET(void);
/* Private functions ---------------------------------------------------------*/ 


void Delay(__IO uint32_t nTime)
{ 
  TimingDelay = nTime;

  while(TimingDelay != 0x00);
}



/**
  * @brief  Decrements the TimingDelay variable.
  * @param  None
  * @retval None
  */
void TimingDelay_Decrement(void)
{
  if (TimingDelay != 0x00)
  { 
    TimingDelay--;
  }
}

void USART2_Init(void){

  /* USART2 clock enable */
RCC_APB1PeriphClockCmd(RCC_APB1Periph_USART2,ENABLE); 
  /* Enable GPIOs clock */ 	
RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOA , ENABLE); 
 
GPIO_InitStructure.GPIO_Pin = GPIO_Pin_2 | GPIO_Pin_3; 
GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF; 
GPIO_InitStructure.GPIO_OType = GPIO_OType_PP; 
GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_NOPULL; 
GPIO_InitStructure.GPIO_Speed = GPIO_Speed_2MHz; 
GPIO_Init(GPIOA, &GPIO_InitStructure); 
/* Connect UART pins to AF */ 
GPIO_PinAFConfig(GPIOA, GPIO_PinSource2, GPIO_AF_USART2); 
GPIO_PinAFConfig(GPIOA, GPIO_PinSource3, GPIO_AF_USART2);

USART_InitTypeDef USART_InitStructure;
USART_InitStructure.USART_BaudRate =115200; 
USART_InitStructure.USART_WordLength = USART_WordLength_8b; 
USART_InitStructure.USART_StopBits = USART_StopBits_1; 
USART_InitStructure.USART_Parity = USART_Parity_No ; 
USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;
USART_InitStructure.USART_Mode = USART_Mode_Tx | USART_Mode_Rx; 
USART_Init(USART2, &USART_InitStructure); 
USART_Cmd(USART2, ENABLE); /*enable usart2 */
}
/*----------------------------------------------------------------------------*/

void delay(__IO uint32_t nCount)  
{  
  for(int i = nCount ; i>=0 ; i--){
  
  }
   
}

 int main(void){
    if (SysTick_Config(SystemCoreClock / 1000))
  { 
    /* Capture error */ 
    while (1);
  }
  
 USART2_Init();
 VuMeter_Init();
 ElectroPumps_Init();
 

 while(1){
 data=USART2GET();
 lin=(data>>4)&0x03;
 Lvl=(data & 0x0F);
 SleepMode = (data>>6)&0x01;
 if(!SleepMode)
 VuMeter_Display(Lvl,lin);
   ElectroPumps_Handler(lin,Lvl);
 }
}
/*----------------------------------------------------------------------------*/
PUTCHAR_PROTOTYPE
{
  /* Loop until the end of transmission */
  /* Place your implementation of fputc here */
  /* e.g. write a character to the USART */
  USART_SendData(USART2, (uint8_t) ch); 

  while (USART_GetFlagStatus(USART2, USART_FLAG_TC) == RESET)
  {}

  return ch;
} 


uint8_t USART2GET(void){
while ( USART_GetFlagStatus(USART2, USART_FLAG_RXNE) == RESET);
     return (uint8_t)USART_ReceiveData(USART2);
}






#ifdef  USE_FULL_ASSERT

/**
  * @brief  Reports the name of the source file and the source line number
  *         where the assert_param error has occurred.
  * @param  file: pointer to the source file name
  * @param  line: assert_param error line source number
  * @retval None
  */
void assert_failed(uint8_t* file, uint32_t line)
{ 
  /* User can add his own implementation to report the file name and line number,
     ex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */

  /* Infinite loop */
  while (1)
  {
  }
}
#endif
