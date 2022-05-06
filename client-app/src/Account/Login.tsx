import React, { SyntheticEvent, useState } from 'react';

const Login = (
    ) => 
{
    
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [resultcode, setResultcode] = useState(0);
    const [resultTitle, setResultTitle] = useState('');



    const submit = async (e: SyntheticEvent) => {
        e.preventDefault();
        const response = await fetch('https://localhost:5001/account/login',
        {
            method : 'POST',
            headers:{'Content-Type' : 'application/json'},
            body: JSON.stringify({
                email,
                password
            })
        });
        const content = await response.json();
        const status = await response.status

        setResultcode(status);
        setResultTitle(content.title);
        if(status==200){
            setName(content.username);

        }

    }

    
    return (
        <>
        <form onSubmit={submit}>
            <h2>Sign in</h2>

            <ul>

                <li>
                    <label>email</label>
                    <input type="email" placeholder="name@example.com" required 
                        onChange = {e => setEmail(e.target.value)}            
                    />
                </li>

                <li>                    
                    <label>password</label>
                    <input type="password" placeholder="Password" required 
                        onChange = {e => setPassword(e.target.value)}            
                    />
                </li>
            </ul>

            <button type="submit">Sign in</button>

        </form>
        
        <h2>Response</h2>

        <ul>
            <li>
                {resultcode!=0 && <>{resultcode==200 ? <>Login Success</> : <>Login Fail</>}</>}
            </li>

            <li>
                {resultcode==200 && <>Name:{name}</>}
            </li>

            <li>
                {resultcode!=0 && <>Code:{resultcode}</>}
            </li>

            <li>
                {resultcode!=0 && <>msg:{resultTitle}</>}
            </li>
        </ul>
        </>
    );

}

export default Login;