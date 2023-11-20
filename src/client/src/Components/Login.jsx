import React from 'react'
import './Login.css'
import Title from './Title.png'
import Logo from './Chrono Master.png'

export const Login = () => {
  return (
    <div>
        <div className="logo">
            <img src={Logo} width={230} alt="" />
        </div>
        <div className="container">
            <div className="header">
                <img src={Title} alt="" />
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
