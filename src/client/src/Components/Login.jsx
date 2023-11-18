import React from 'react'
import './Login.css'

export const Login = () => {
  return (
    <div>
        <div className="container">
            <div className="header">
                <div className="text">Gerer vos cours <br />  comme jamais</div>
            </div>
            <div className="inputs">
                <span> Votre Matricule</span>
                <div className="input">
                    <input type="text" placeholder='17G00352' />
                </div>
                <span>  Votre Mot de passe </span>
                <div className="input">
                    <input type="password" placeholder='*******' />
                </div>
            </div>
        </div>
        <div className="button">
            <input type="radio" name='button' />j'ai lu et j'accepte les condition generales d'utilisation
        </div>
        <div className="submit-container">
            <div className="submit">connexion</div>
        </div>
    </div>
  )
}
