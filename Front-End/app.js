const apiUrl = "https://localhost:7011/api/user";

async function renderUsers() {
  const users = await getUsers();
  const userListDiv = document.getElementById("userList");
  userListDiv.innerHTML = "";

  users.forEach((user) => {
    const userCard = document.createElement("div");
    userCard.style.border = "1px solid #ccc";
    userCard.style.margin = "10px 0";
    userCard.style.padding = "10px";
    userCard.style.display = "block";

    userCard.innerHTML = `
      <div><strong>Nome:</strong> ${user.firstName} ${user.lastName}</div>
      <div><strong>Email:</strong> ${user.email}</div>
      <div><strong>Gênero:</strong> ${user.gender}</div>
      <div><strong>Telefone:</strong> ${user.phone}</div>
      <div><strong>Celular:</strong> ${user.cell}</div>
      <div><strong>Rua:</strong> ${user.streetName}, ${user.streetNumber}</div>
      <div><strong>CEP:</strong> ${user.postCode}</div>
      <div><strong>Cidade:</strong> ${user.city}</div>
      <div><strong>Estado:</strong> ${user.state}</div>
      <div><strong>País:</strong> ${user.country}</div>
      <div>
        <img src="${user.profilePicture}" alt="Foto de perfil" width="50">
      </div>
      <div>
        <button class="edit-btn" onclick="editUser(${user.id})">Editar</button>
        <button class="delete-btn" onclick="deleteUser(${user.id})">Deletar</button>
      </div>
    `;
    userListDiv.appendChild(userCard);
  });
}

async function getUsers() {
  try {
    const response = await fetch(apiUrl);
    return await response.json();
  } catch (error) {
    console.error("Erro ao obter os usuários:", error);
  }
}

async function addUser(user) {
  try {
    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(user),
    });

    if (response.ok) {
      alert("Usuário adicionado com sucesso!");
      renderUsers();
    } else {
      const errorText = await response.text();
      alert("Erro ao adicionar o usuário: " + errorText);
      console.error("Erro ao adicionar o usuário:", errorText);
    }
  } catch (error) {
    console.error("Erro ao adicionar o usuário:", error);
  }
}

async function updateUser(id, user) {
  try {
    user.id = id;
    const response = await fetch(`${apiUrl}/${id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(user),
    });

    if (response.ok) {
      alert("Usuário atualizado com sucesso!");
      renderUsers();
    } else {
      const errorText = await response.text();
      alert("Erro ao atualizar o usuário: " + errorText);
      console.error("Erro ao atualizar o usuário:", errorText);
    }
  } catch (error) {
    console.error("Erro ao atualizar o usuário:", error);
  }
}

async function deleteUser(id) {
  try {
   
    const confirmDelete = confirm("Tem certeza que deseja deletar os dados do usuário?");
    
    if (!confirmDelete) {
     
      return;
    }

    const response = await fetch(`${apiUrl}/${id}`, {
      method: "DELETE",
    });

    if (response.ok) {
      alert("Usuário deletado com sucesso!");
      renderUsers();
    } else {
      alert("Erro ao deletar o usuário");
    }
  } catch (error) {
    console.error("Erro ao deletar o usuário:", error);
  }
}


async function editUser(id) {
  const user = await getUserById(id);

 
  document.getElementById("userId").value = user.id;
  document.getElementById("firstName").value = user.firstName;
  document.getElementById("lastName").value = user.lastName;
  document.getElementById("email").value = user.email;
  document.getElementById("gender").value = user.gender;
  document.getElementById("phone").value = user.phone;
  document.getElementById("cell").value = user.cell;
  document.getElementById("streetName").value = user.streetName;
  document.getElementById("streetNumber").value = user.streetNumber;
  document.getElementById("postCode").value = user.postCode;
  document.getElementById("city").value = user.city;
  document.getElementById("state").value = user.state;
  document.getElementById("country").value = user.country;
  document.getElementById("profilePicture").value = user.profilePicture;
  document.getElementById("formTitle").innerText = "Editar Usuário";

  
  const formElement = document.getElementById("userForm");
  formElement.scrollIntoView({ behavior: "smooth", block: "start" });
}


async function getUserById(id) {
  try {
    const response = await fetch(`${apiUrl}/${id}`);
    if (!response.ok) {
      throw new Error("Usuário não encontrado");
    }
    return await response.json();
  } catch (error) {
    console.error("Erro ao obter o usuário:", error);
  }
}

document.getElementById("userForm").addEventListener("submit", async (event) => {
  event.preventDefault();

  const userId = document.getElementById("userId").value;

  const user = {
    firstName: document.getElementById("firstName").value,
    lastName: document.getElementById("lastName").value,
    email: document.getElementById("email").value,
    gender: document.getElementById("gender").value,
    phone: document.getElementById("phone").value,
    cell: document.getElementById("cell").value,
    streetName: document.getElementById("streetName").value,
    streetNumber: parseInt(document.getElementById("streetNumber").value),
    postCode: document.getElementById("postCode").value,
    city: document.getElementById("city").value,
    state: document.getElementById("state").value,
    country: document.getElementById("country").value,
    profilePicture: document.getElementById("profilePicture").value || "",
  };

  if (userId) {
    await updateUser(userId, user);
  } else {
    await addUser(user);
  }

  document.getElementById("userForm").reset(); 
  document.getElementById("userId").value = ""; 
  document.getElementById("formTitle").innerText = "Adicionar Usuário"; 
});

renderUsers();
