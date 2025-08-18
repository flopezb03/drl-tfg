# APRENDIZAJE POR REFUERZO CON ENTRENAMIENTO PARALELO DE UN COCHE DE CARRERAS

- Deep Reinforcement Learning
- Proximal Policy Optimization

## Objetivos del proyecto
El objetivo final del proyecto es desarrollar y entrenar un agente inteligente capaz de controlar un coche por un circuito (en un entorno simulado) lo más rápido posible. Este objetivo se divide en los siguientes sub-objetivos:

1. Diseñar las características y movimiento del coche en el entorno simulado. Para ello se utilizada el motor de videojuegos de **Unity**.
2. Diseñar el entorno de aprendizaje por refuerzo dividiendo el aprendizaje en escenarios. Se utiliza el proyecto open-source de **Unity Machine Learning Agentes Toolkit** ([ML-Agents Toolkit](https://github.com/Unity-Technologies/ml-agents/tree/develop)).
3. Ajuste de las configuraciones del entrenamiento y los hiperparámetros del algoritmo utilizado para disminuir los tiempos de entrenamiento.
4. Entrenar al agente logrando que cumpla sus propios objetivos.

Por otro lado, el agente tiene sus propios objetivos que son superar eficazmente los escenarios de aprendizaje. Una vez haya superado todos, el agente deberá ser capaz de:

- Controlar el coche sin salirse del recorrido.
- Seguir los checkpoints del recorrido.
- Diferenciar los tipos de checkpoints para anticiparse al siguiente tramo y mejorar su velocidad total.
- Adaptar su forma de conducir al estado de las ruedas.
- Conducir rápido.

## Resultados

Video con el desarrollo del agente a través de sus entrenamientos:
[![Evolución del entrenamiento](https://img.youtube.com/vi/d6Tem2h2rn0/hqdefault.jpg)](https://youtu.be/d6Tem2h2rn0)

Con el ajuste de las configuraciones para el entrenamiento y los hiperparámetros del algoritmo se ha logrado una mejora muy significativa de los tiempos empleados para los entrenamientos. Antes de ajustar ninguna configuración, uti- lizando los parámetros por defecto, el agente necesitaba un entrenamiento de más de **2 horas** en el _primer escenario_ para completarlo. En cambio, al final, el agente tarda **∼1.5 minutos**.

## Documentación

Para más detalles sobre el diseño, entrenamiento y resultados obtenidos, consulta la memoria completa en:

[📘 Memoria del TFG](./doc/tfg_memoria.pdf)
