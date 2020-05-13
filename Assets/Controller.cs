using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class Controller : MonoBehaviour
{

    public float speed = 6.0f;
    private GameObject cameraFPS;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private float rotacaoX = 0;
    private float rotacaoY = 0.0f;

    void Start()
    {
        cameraFPS = GetComponentInChildren(typeof(Camera)).transform.gameObject;
        cameraFPS.transform.localPosition = new Vector3(0,1,0);
        cameraFPS.transform.localRotation = Quaternion.identity;
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        //apenas movimenta o jogador se ele estiver no chão
        if (controller.isGrounded) {
            //pega a direção da face à frente da camera
            Vector3 direcaoFrente = new Vector3(cameraFPS.transform.forward.x, 0, cameraFPS.transform.forward.z);
            //pega a direção da face ao lado da camera
            Vector3 direcaoLado = new Vector3(cameraFPS.transform.right.x, 0, cameraFPS.transform.right.z);
            //normaliza os valores para o máximo de 1, para que o jogador ande sempre na mesma velocidade
            direcaoFrente.Normalize();
            direcaoLado.Normalize();

            //pega o valor das teclas para cima (1) e para baixo  (-1)
            direcaoFrente = direcaoFrente * Input.GetAxis("Vertical");
            //pega o valor das teclas para direita (1) e para esquerda  (-1)
            direcaoLado = direcaoLado * Input.GetAxis("Horizontal");

            //soma a movimentação lateral com a movimentação para frente/trás
            Vector3 direcaoFinal = direcaoFrente + direcaoLado;
            if (direcaoFinal.sqrMagnitude>1) {
                direcaoFinal.Normalize();
            }
            
            //apenas move nas direções x e z
            moveDirection = new Vector3(direcaoFinal.x, 0, direcaoFinal.z);

            //multiplica pela velocidade que foi configurada no jogador
            moveDirection = moveDirection * speed;
            
            //pular
            if (Input.GetButton(("Jump"))) {
                moveDirection.y = 8.0f;
            }



        }
        //faz o jogador ir pra baixo (gravidade)
        moveDirection.y -= 20.0f * Time.deltaTime;

        //faz o movimento
        controller.Move(moveDirection * Time.deltaTime);


    }
}