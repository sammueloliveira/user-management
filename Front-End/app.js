const apiUrl = "https://localhost:7011/api/user";

async function renderUsers() {
  const users = await getUsers();
  const userListDiv = document.getElementById("userList");
  userListDiv.innerHTML = "";

  users.forEach((user) => {
    const userRow = document.createElement("tr");
    userRow.innerHTML = `
      <td>${user.firstName} ${user.lastName}</td>
      <td>${user.email}</td>
      <td>${user.city}</td>
      <td>${user.gender}</td>
      <td>${user.country}</td>
      <td><img src="${user.profilePicture}" alt="Foto de perfil" width="50"></td>
      <td>
        <button class="edit-btn" onclick="editUser(${user.id})">Editar</button>
        <button class="delete-btn" onclick="deleteUser(${user.id})">Deletar</button>
      </td>
    `;
    userListDiv.appendChild(userRow);
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
      alert("Erro ao adicionar o usuário");
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
      console.log("Erro ao atualizar o usuário: " + errorText);
    }
  } catch (error) {
    console.error("Erro ao atualizar o usuário:", error);
  }
}

async function deleteUser(id) {
  try {
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
  document.getElementById("city").value = user.city;
  document.getElementById("country").value = user.country;
  document.getElementById("profilePicture").value = user.profilePicture;
  document.getElementById("formTitle").innerText = "Editar Usuário";
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

document
  .getElementById("userForm")
  .addEventListener("submit", async (event) => {
    event.preventDefault();

    const user = {
      firstName: document.getElementById("firstName").value,
      lastName: document.getElementById("lastName").value,
      email: document.getElementById("email").value,
      gender: document.getElementById("gender").value,
      city: document.getElementById("city").value,
      country: document.getElementById("country").value,
      profilePicture: document.getElementById("profilePicture").value,
    };

    const userId = document.getElementById("userId").value;

    if (userId) {
      await updateUser(userId, user);
    } else {
      await addUser(user);
    }

    document.getElementById("userForm").reset();
  });

renderUsers();
